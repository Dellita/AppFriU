using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
/* RateMe.cs
 *
 * Usage:
 *      Place the following code somewhere during page initialization:
 *
 *      var rateMe = new AppFriU.RateMe();
 *      rateMe.CheckReviewsAsync();
 */
namespace AppFriU
{
    class RateMe
    {
        static private int REVIEWS_BARRIER = 5;
        static private string REVIEW_TEXT = "Надеемся, вам нравится наше приложение. Пожалуйста, оцените наше приложение.";
        static private string REVIEW_INVITE_TEXT = "Оценить";
        static private string REVIEW_DECLINE_TEXT = "Напомнить позже";

        public RateMe()
        {
            this.LoadReviewStatus();
        }

        private ApplicationDataCompositeValue status;
        private void LoadReviewStatus()
        {
            var roamingSettings = ApplicationData.Current.RoamingSettings;
            var reviewStatus = (ApplicationDataCompositeValue)roamingSettings.Values["reviewStatus"];
      
          if (reviewStatus == null)
            {
             reviewStatus = new ApplicationDataCompositeValue();
                reviewStatus["userReviewedApp"] = false;
                reviewStatus["userReviewAppLaunches"] = 0;
            }

            this.status = reviewStatus;
        }
        private void SaveReviewStatus()
        {
            var roamingSettings = ApplicationData.Current.RoamingSettings;
            roamingSettings.Values["reviewStatus"] = this.status;
        }
        public async void CheckReviewsAsync()
        {
            if (this.status == null)
            {
                this.LoadReviewStatus();
            }
            var reviewed = (bool)this.status["userReviewedApp"];
            var launches = (int)this.status["userReviewAppLaunches"];

            if (!reviewed && launches > 0 && launches % RateMe.REVIEWS_BARRIER == 0)
            {
                await this.AskForReviewAsync();
            }
            this.status["userReviewAppLaunches"] = ++launches;
            this.SaveReviewStatus();
        }
        private async Task AskForReviewAsync()
        {
            var messageDialog = new Windows.UI.Popups.MessageDialog(RateMe.REVIEW_TEXT);
      
            messageDialog.Commands.Add(new Windows.UI.Popups.UICommand(RateMe.REVIEW_INVITE_TEXT,
                async (action) =>
                {
                 

                    //     await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store:reviewapp?appid=" + Windows.ApplicationModel.Package.Current.Id.FamilyName));
                    //   var uri = new Uri("ms-windows-store:Review?PFN=" + Windows.ApplicationModel.Package.Current.Id.FamilyName);
#if WINDOWS_PHONE_81 
                    var uri = new Uri("ms-windows-store:reviewapp?appid=89432f6e-6ecb-4287-bbb5-a58c0d4b9d7e");
#else 
                    var uri = new Uri("ms-windows-store:Review?PFN=" + Windows.ApplicationModel.Package.Current.Id.FamilyName);
#endif
                    //     uri = new Uri("ms-windows-store://review/?AppId=89432f6e-6ecb-4287-bbb5-a58c0d4b9d7e");
                    await Windows.System.Launcher.LaunchUriAsync(uri);
                    this.status["userReviewedApp"] = true;
                    this.SaveReviewStatus();
                }
                ));
            messageDialog.Commands.Add(new Windows.UI.Popups.UICommand(RateMe.REVIEW_DECLINE_TEXT, null));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;
      
            await messageDialog.ShowAsync();
        }
    }
}
