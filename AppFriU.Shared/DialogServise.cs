using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace AppFriU
{
    public class DialogServise
    {
        private static bool isShowing = false;

        public static async Task ShowAlertAsync(String title, String body)
        {
            // Do not open a new dialog if another one exists
            if (isShowing)
                return;

#if WINDOWS_PHONE_APP
             
            ContentDialog dialog = new ContentDialog();
   
            dialog.Title = title;
            dialog.PrimaryButtonText = "OK";
            dialog.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            dialog.VerticalContentAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            var color = Global.DIALOG_COLOR;
            color.Opacity = 0.7;
            dialog.Background = color;
            dialog.Margin = new Windows.UI.Xaml.Thickness(0, 0, 0, 0);
            dialog.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            dialog.Content = new TextBlock()
            {
                Text = body,
                TextWrapping = Windows.UI.Xaml.TextWrapping.WrapWholeWords,
                FontSize = 40,
                TextAlignment = Windows.UI.Xaml.TextAlignment.Center,
            
            };

            DialogServise.isShowing = true;
            await dialog.ShowAsync();
            DialogServise.isShowing = false;
#else

#endif
        }
    }

  
}
