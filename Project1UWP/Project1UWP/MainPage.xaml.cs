using Project1UWP.Data;
using Project1UWP.Models;
using Project1UWP.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Project1UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly ILeagueRepository leagueRepository;
        private readonly ITeamRepository teamRepository;

        public MainPage()
        {
            this.InitializeComponent();
            leagueRepository = new LeagueRepository();
            teamRepository = new TeamRepository();
            FillDropDown();
        }

        private async void FillDropDown()
        {
            progRing.IsActive = true;
            progRing.Visibility = Visibility.Visible;

            try
            {
                List<League> leagues = await leagueRepository.GetLeagues();
                leagues.Insert(0, new League { Code = "", Name = "All Leagues" });
                cboLeague.ItemsSource = leagues;
                btnAdd.IsEnabled = true;
                ShowTeams(null);
            }
            catch(Exception ex)
            {
                if(ex.GetBaseException().Message.Contains("connection with server"))
                {
                    Jeeves.ShowMessage("Error", "No connection with the server");
                }
                else
                {
                    Jeeves.ShowMessage("Error", "Could not complete operation");
                }
            }
        }

        private async void ShowTeams(string leagueCode)
        {
            progRing.IsActive = true;
            progRing.Visibility = Visibility.Visible;

            try
            {
                List<Team> teams;
                
                //if(leagueCode.GetValueOrDefault() != "")
                if(leagueCode == null || leagueCode == "")
                {
                    teams = await teamRepository.GetTeams();
                    
                }
                else
                {
                    teams = await teamRepository.GetTeamsByLeague(leagueCode);
                }


                teamList.ItemsSource = teams;
            }
            catch(Exception ex)
            {
                if(ex.GetBaseException().Message.Contains("connection with the server"))
                {
                    Jeeves.ShowMessage("Error", "No connection with the server.");
                }
                else
                {
                    Jeeves.ShowMessage("Error", "Could not complete operation");
                }
            }
            finally
            {
                progRing.IsActive = false;
                progRing.Visibility = Visibility.Collapsed;
            }
        }
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            FillDropDown();
        }

        private void cboLeague_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            League selLeague = (League)cboLeague.SelectedItem;
            ShowTeams(selLeague?.Code);
        }

        private void teamGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(TeamDetailPage), (Team)e.ClickedItem);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Team newTeam = new Team();

            Frame.Navigate(typeof(TeamDetailPage), newTeam);
        }
    }
}
