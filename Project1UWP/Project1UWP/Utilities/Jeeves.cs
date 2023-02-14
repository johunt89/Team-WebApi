using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Project1UWP.Utilities
{
    public static class Jeeves
    {
        //local api uri
        public static Uri DBUri = new Uri("https://localhost:44359/");

        public static ApiException CreateApiException(HttpResponseMessage response)
        {
            var httpErrorObject = response.Content.ReadAsStringAsync().Result;

            //creates an anonymous object to use as the template for deserialization:
            var anonymousErrorObject = new { message = "", errors = new Dictionary<string, string[]>() };

            //desesrializes:
            var deserializedErrorObject = JsonConvert.DeserializeAnonymousType(httpErrorObject, anonymousErrorObject);

            var ex = new ApiException(response);

            if(deserializedErrorObject.message != null)
            {
                ex.Data.Add(-1, deserializedErrorObject.message);
            }

            if(deserializedErrorObject.errors != null)
            {
                foreach(var err in deserializedErrorObject.errors)
                {
                    ex.Data.Add(err.Key, err.Value[0]);
                }
            }
            return ex;
        }

        internal static async void ShowMessage(string strTitle, string Msg)
        {
            ContentDialog diag = new ContentDialog()
            {
                Title = strTitle,
                Content = Msg,
                PrimaryButtonText = "Ok",
                DefaultButton = ContentDialogButton.Primary
            };
            _ = await diag.ShowAsync();
        }
        internal static async Task<ContentDialogResult> ConfirmDialog(string strTitle, string Msg)
        {
            ContentDialog diag = new ContentDialog()
            {
                Title = strTitle,
                Content = Msg,
                PrimaryButtonText = "No",
                SecondaryButtonText = "Yes",
                DefaultButton = ContentDialogButton.Primary
            };
            ContentDialogResult result = await diag.ShowAsync();
            return result;
        }
    }
}
