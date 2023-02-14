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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Project1UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TeamDetailPage : Page
    {
        Team view;
        ITeamRepository teamRepository;
        ILeagueRepository leagueRepository;
        bool InsertMode;

        public TeamDetailPage()
        {
            this.InitializeComponent();
            teamRepository = new TeamRepository();
            leagueRepository = new LeagueRepository();
            fillDropDown();
        }

        private async void fillDropDown()
        {
            try
            {
                List<League> leagues = await leagueRepository.GetLeagues();

                LeagueCombo.ItemsSource = leagues.OrderBy(n => n.Name);

                this.DataContext = view;
            }
            catch(Exception ex)
            {
                if(ex.GetBaseException().Message.Contains("connection with the server"))
                {
                    Jeeves.ShowMessage("Error", "No connection with the server.");
                }
                else
                {
                    Jeeves.ShowMessage("Error", "Could not complete operation.");
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            view = (Team)e.Parameter;
            
            if(view.ID == 0)
            {
                btnDelete.IsEnabled = false; ;
                InsertMode = true;
            }
            else
            {
                InsertMode = false;
            }
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(view.LeagueCode == "" || view.LeagueCode == null)
                {
                    Jeeves.ShowMessage("Error", "You must select a league");
                }
                else
                {
                    if (InsertMode)
                    {
                        await teamRepository.AddTeam(view);
                    }
                    else
                    {
                        await teamRepository.UpdateTeam(view);
                    }
                    Frame.GoBack();
                }
            }
            catch(AggregateException aex)
            {
                string errMsg = "Errors:" + Environment.NewLine;
                foreach(var exception in aex.InnerExceptions)
                {
                    errMsg += Environment.NewLine + exception.Message;
                }
                Jeeves.ShowMessage("One or more exceptions has occred:", errMsg);
            }
            catch (ApiException apiEx)
            {
                string errMsg = "Errors:" + Environment.NewLine;
                foreach (var error in apiEx.Errors)
                {
                    errMsg += Environment.NewLine + "-" + error;
                }
                Jeeves.ShowMessage("Problem saving record:", errMsg);
            }
            catch(Exception ex)
            {
                if(ex.GetBaseException().Message.Contains("connection with server"))
                {
                    Jeeves.ShowMessage("Error", "No connection with the server.");
                }
                else
                {
                    Jeeves.ShowMessage("Error", "Could not complete operation.");
                }
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string strTitle = "Confirm Delete";
            string strMsg = "Are you sure you want to delete " + view.Name + "?";
            ContentDialogResult result = await Jeeves.ConfirmDialog(strTitle, strMsg);
            if(result == ContentDialogResult.Secondary)
            {
                try
                {
                    await teamRepository.DeleteTeam(view);
                    Frame.GoBack();
                }
                catch(AggregateException aex)
                {
                    string errMsg = "Errors:" + Environment.NewLine;
                    foreach (var exception in aex.InnerExceptions)
                    {
                        errMsg += Environment.NewLine + exception.Message;
                    }
                    Jeeves.ShowMessage("One or more exceptions has occured:", errMsg);
                }
                catch (ApiException apiEx)
                {
                    string errMsg = "Errors:" + Environment.NewLine;
                    foreach (var error in apiEx.Errors)
                    {
                        errMsg += Environment.NewLine + "-" + error;
                    }
                    Jeeves.ShowMessage("Problem deleting record:", errMsg);
                }
                catch (Exception ex)
                {
                    if (ex.GetBaseException().Message.Contains("connection with server"))
                    {
                        Jeeves.ShowMessage("Error", "No connection with the server.");
                    }
                    else
                    {
                        Jeeves.ShowMessage("Error", "Could not complete operation.");
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
