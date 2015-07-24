using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Fantasy.Metro.Controls
{
    public class FantasyWizard : Control
    {
        public FantasyWizard()
        {
            this.DefaultStyleKey = typeof(FantasyWizard);
            this.SelectedIndex = 0;
            SetCurrentValue(LinksProperty, new LinkCollection());
        }

        public IContentLoader ContentLoader
        {
            get { return (IContentLoader)GetValue(ContentLoaderProperty); }
            set { SetValue(ContentLoaderProperty, value); }
        }

        public static readonly DependencyProperty ContentLoaderProperty =
            DependencyProperty.Register("ContentLoader",
                typeof(IContentLoader),
                typeof(FantasyWizard),
                new PropertyMetadata(new DefaultContentLoader()));

        public Thickness ContentMargin
        {
            get { return (Thickness)GetValue(ContentMarginProperty); }
            set { SetValue(ContentMarginProperty, value); }
        }

        public static readonly DependencyProperty ContentMarginProperty =
            DependencyProperty.Register("ContentMargin",
                typeof(Thickness),
                typeof(FantasyWizard),
                new PropertyMetadata(new Thickness(0, 0, 0, 0)));

        public LinkCollection Links
        {
            get { return (LinkCollection)GetValue(LinksProperty); }
            set { SetValue(LinksProperty, value); }
        }

        public static readonly DependencyProperty LinksProperty =
            DependencyProperty.Register("Links",
                typeof(LinkCollection),
                typeof(FantasyWizard),
                new PropertyMetadata(FantasyWizard.OnLinksChanged));

        public Uri SelectedSource
        {
            get { return (Uri)GetValue(SelectedSourceProperty); }
            set { SetValue(SelectedSourceProperty, value); }
        }

        public static readonly DependencyProperty SelectedSourceProperty =
            DependencyProperty.Register(
                "SelectedSource",
                typeof(Uri),
                typeof(FantasyWizard),
                new PropertyMetadata(FantasyWizard.OnSelectedSourceChanged));

        public event EventHandler<SourceEventArgs> SelectedSourceChanged;
        public event EventHandler OnCanceled;
        public event EventHandler OnCompleted;
        
        private static void OnSelectedSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            FantasyWizard wizard = sender as FantasyWizard;
            Uri from = e.OldValue as Uri;
            Uri to = e.NewValue as Uri;
            wizard.OnSelectedSourceChanged(from, to);
        }

        private void OnSelectedSourceChanged(Uri from, Uri to)
        {
            UpdateSelection();
            if (this.SelectedSourceChanged != null)
            {
                this.SelectedSourceChanged(this, new SourceEventArgs(from, to));
            }
        }

        private static void OnLinksChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            FantasyWizard wizard = sender as FantasyWizard;
            wizard.UpdateSelection();
        }

        private void UpdateSelection()
        {
            if (this.LinkList == null || this.Links == null)
            {
                return;
            }

            // sync list selection with current source
            Link link = this.Links.FirstOrDefault(l => l.Source == this.SelectedSource);
            this.LinkList.SelectedItem = link;
        }

        private void OnLinkListSelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            Link link = this.LinkList.SelectedItem as Link;
            if (link != null && link.Source != this.SelectedSource)
            {
                this.SelectedIndex = this.LinkList.SelectedIndex;
                SetCurrentValue(SelectedSourceProperty, link.Source);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.LinkList != null)
                this.LinkList.SelectionChanged -= OnLinkListSelectionChanged;

            if (this.CancelButton != null)
                this.CancelButton.Click -= this.OnCancelClick;

            if (this.OkButton != null)
                this.OkButton.Click -= this.OnOkClick;

            if (this.NextButton != null)
                this.NextButton.Click -= this.OnNextClick;

            if (this.PreviousButton != null)
                this.PreviousButton.Click -= this.OnPreviousClick;

            if (this.PresentingFrame != null)
                this.PresentingFrame.Navigated -= this.OnContentNavigated;
            
            this.LinkList = (ListBox)GetTemplateChild("LinkList");
            this.LinkList.SelectionChanged += OnLinkListSelectionChanged;                

            this.WizardTitleTextBlock = (TextBlock)GetTemplateChild("TbWizardTitle");
            //this.PresentingContent = (FantasyContentControl)GetTemplateChild("PresentingContent");
            this.PresentingFrame = (FantasyFrame)GetTemplateChild("PresentingFrame");
            this.PresentingFrame.Navigated += this.OnContentNavigated;
            
            this.OkButton = (Button)GetTemplateChild("BtnOK");
            this.OkButton.Click += this.OnOkClick;
            
            this.FinishButton = (Button)GetTemplateChild("BtnFinish");
            this.FinishButton.Click += this.OnFinishClick;
            
            this.NextButton = (Button)GetTemplateChild("BtnNext");
            this.NextButton.Click += this.OnNextClick;

            this.PreviousButton = (Button)GetTemplateChild("BtnPrevious");
            this.PreviousButton.Click += this.OnPreviousClick;

            this.CancelButton = (Button)GetTemplateChild("BtnCancel");
            this.CancelButton.Click += this.OnCancelClick;

            this.UpdateSelection();
        }

        private void OnContentNavigated(Object sender, NavigationEventArgs e)
        {
            this.WizardTitleTextBlock.Text = String.Empty;
            IFantasyWizardContent currentContent = this.PresentingFrame.Content as IFantasyWizardContent;
            if (currentContent != null)
            {
                this.WizardTitleTextBlock.Text = currentContent.Title;
            }
        }

        private void OnFinishClick(Object sender, EventArgs e) 
        {
            this.SelectedIndex = this.Links.Count - 1;
            this.LinkList.SelectedIndex = this.SelectedIndex;
            this.FinishButton.Visibility = System.Windows.Visibility.Collapsed;
            this.OkButton.Visibility = System.Windows.Visibility.Visible;
        }

        private void OnNextClick(Object sender, EventArgs e)
        {
            if (this.SelectedIndex == this.Links.Count - 1)
            {
                return;
            }

            this.SelectedIndex++;
            this.LinkList.SelectedIndex = this.SelectedIndex;

            if (this.SelectedIndex == this.Links.Count - 1)
            {
                this.OkButton.Visibility = Visibility.Visible;
                this.FinishButton.Visibility = Visibility.Collapsed;
            }
        }

        private void OnPreviousClick(Object sender, EventArgs e)
        {
            this.OkButton.Visibility = Visibility.Collapsed;
            this.FinishButton.Visibility = Visibility.Visible;

            if (this.SelectedIndex == 0)
                return;

            this.SelectedIndex--;
            this.LinkList.SelectedIndex = this.SelectedIndex;
        }

        private void OnOkClick(Object sender, EventArgs e)
        {
            if (this.OnCompleted != null)
                this.OnCompleted(sender, e);
        }

        private void OnCancelClick(Object sender, EventArgs e)
        {
            if (this.OnCanceled != null)
                this.OnCanceled(sender, e);
        }

        private TextBlock WizardTitleTextBlock { get; set; }
        private ListBox LinkList { get; set; }
        //private FantasyContentControl PresentingContent { get; set; }
        private FantasyFrame PresentingFrame { get; set; }
        private Button OkButton { get; set; }
        private Button FinishButton { get; set; }
        private Button CancelButton { get; set; }
        private Button NextButton { get; set; }
        private Button PreviousButton { get; set; }
        private int SelectedIndex { get; set; }
    }
}
