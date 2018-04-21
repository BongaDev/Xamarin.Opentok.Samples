using System;
using DT.Samples.Opentok.Shared;
using DT.Samples.Opentok.Shared.Helpers;
using Foundation;
using UIKit;

namespace DT.Samples.Opentok.OneToOne.iOS
{
    public partial class JoinViewController : UIViewController
    {
        protected const string QualityFormat = "Current Connection - {0}";
        protected const string OpentokVersion = "powered by OpenTok <version>";

        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            return UIStatusBarStyle.LightContent;
        }

        protected JoinViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ChannelNameEdit.Text = OpentokSettings.Current.RoomName;
            ChannelNameEdit.EditingDidBegin += TextField_EditingDidBegin;
            ChannelNameEdit.EditingDidEnd += TextField_EditingDidEnd;
            MakeTextFieldRounded(ChannelNameEdit);
            SDKVersionLabel.Text = OpentokVersion;
            NavigationItem.LeftBarButtonItem = new UIBarButtonItem(UIImage.FromBundle("ic_share"), UIBarButtonItemStyle.Plain, ShareButtonCliked);
            SetupKeyboardHiding();
        }

        public override void ViewDidDisappear(bool animated)
        {
            OpentokSettings.Current.RoomName = ChannelNameEdit.Text;
            base.ViewDidDisappear(animated);
        }

        private void SetupKeyboardHiding()
        {
            UITapGestureRecognizer singleTapRecognizer = new UITapGestureRecognizer(() => { ChannelNameEdit.ResignFirstResponder(); });
            singleTapRecognizer.NumberOfTouchesRequired = 1;
            singleTapRecognizer.CancelsTouchesInView = false;
            View.AddGestureRecognizer(singleTapRecognizer);
        }

        private void TextField_EditingDidBegin(object sender, EventArgs e)
        {
            ChannelNameEdit.Layer.BorderColor = Theme.TintColor.CGColor;
        }

        private void TextField_EditingDidEnd(object sender, EventArgs e)
        {
            ChannelNameEdit.Layer.BorderColor = Theme.TitleTextColor.CGColor;
        }

        protected void MakeTextFieldRounded(UITextField textField)
        {
            textField.Layer.CornerRadius = 28;
            textField.Layer.BorderColor = Theme.TitleTextColor.CGColor;
            textField.Layer.BorderWidth = 2;
        }

        private void ShareButtonCliked(object sender, EventArgs e)
        {
            var activityController = new UIActivityViewController(new NSObject[] {
                UIActivity.FromObject(OpentokTestConstants.ShareString),
            }, null);
            var topController = UIApplication.SharedApplication.KeyWindow.RootViewController;
            while (topController.PresentedViewController != null)
            {
                topController = topController.PresentedViewController;
            }
            topController.PresentViewController(activityController, true, null);
        }
    }
}

