using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using static Xamarin.Forms.Button.ButtonContentLayout;

namespace StepBarPOC
{
    public class StepProgressBarControl : StackLayout
    {
        public static readonly BindableProperty StepsProperty = BindableProperty.Create(nameof(Steps), typeof(int), typeof(StepProgressBarControl), 0);
        public static readonly BindableProperty StepSelectedProperty = BindableProperty.Create(nameof(StepSelected), typeof(int), typeof(StepProgressBarControl), 0, defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty StepColorProperty = BindableProperty.Create(nameof(StepColor), typeof(Xamarin.Forms.Color), typeof(StepProgressBarControl), Color.Black, defaultBindingMode: BindingMode.TwoWay);

        public Color StepColor
        {
            get { return (Color)GetValue(StepColorProperty); }
            set { SetValue(StepColorProperty, value); }
        }

        public int Steps
        {
            get { return (int)GetValue(StepsProperty); }
            set { SetValue(StepsProperty, value); }
        }

        public int StepSelected
        {
            get { return (int)GetValue(StepSelectedProperty); }
            set { SetValue(StepSelectedProperty, value); }
        }


        public StepProgressBarControl()
        {
            Orientation = StackOrientation.Horizontal;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            Padding = new Thickness(10, 0);
            Spacing = 0;
            AddStyles();
        }

        private void CreateStepBar()
        {
            for (int i = 1; i <= Steps; i++)
            {
                string imgSource = string.Empty;
                Thickness innerCircleMargin = 0;
                string btnText = string.Empty;
                bool showInnerCircle = false;
                Style btnStyle = Resources["unSelectedStyle"] as Style;

                if (i == StepSelected)
                {
                    //To show inner circle                 
                    innerCircleMargin = new Thickness(5, 0, 0, 0);
                    showInnerCircle = true;
                }
                else if (i > StepSelected)
                {
                    //To show number                   
                    btnText = $"{i}";
                }
                else
                {
                    //To show checkmark
                    imgSource = "checkmark.png";
                    btnStyle = Resources["selectedStyle"] as Style;
                }

                var button = new Button()
                {
                    Padding = 0,
                    ImageSource = imgSource,
                    HeightRequest = 30,
                    WidthRequest = 30,
                    IsEnabled = false,
                    Text = btnText,
                    ClassId = $"{i}",
                    Style = btnStyle
                };

                this.Children.Add(button);

                if (showInnerCircle)
                {
                    var innerCircle = new BoxView()
                    {
                        CornerRadius = 15,
                        BackgroundColor = Color.ForestGreen,
                        HeightRequest = 20,
                        WidthRequest = 20,
                        Parent = button,
                        Margin = new Thickness(-25, 0, 0, 0),
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center
                    };
                    this.Children.Add(innerCircle);
                }

                if (i < Steps)
                {
                    var separatorLine = new BoxView()
                    {
                        BackgroundColor = Color.Silver,
                        HeightRequest = 2,
                        WidthRequest = 5,
                        Margin = innerCircleMargin,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.FillAndExpand
                    };
                    this.Children.Add(separatorLine);
                }

            }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == StepSelectedProperty.PropertyName)
            {
                CreateStepBar();

            }
            else if (propertyName == StepColorProperty.PropertyName)
            {
                AddStyles();
            }
        }      

        void AddStyles()
        {
            var unselectedStyle = new Style(typeof(Button))
            {
                Setters = {
                    new Setter { Property = BackgroundColorProperty,   Value = Color.White },
                    new Setter { Property = Button.BorderColorProperty,   Value = StepColor },
                    new Setter { Property = Button.TextColorProperty,   Value = StepColor },
                    new Setter { Property = Button.BorderWidthProperty,   Value = 2 },
                    new Setter { Property = Button.CornerRadiusProperty,   Value = 20 },
                    new Setter { Property = HeightRequestProperty,   Value = 40 },
                    new Setter { Property = WidthRequestProperty,   Value = 40 }
            }
            };

            var selectedStyle = new Style(typeof(Button))
            {
                Setters = {
                    new Setter { Property = BackgroundColorProperty, Value = StepColor },
                    new Setter { Property = Button.TextColorProperty, Value = Color.White },
                    new Setter { Property = Button.BorderColorProperty, Value = StepColor },
                    new Setter { Property = Button.BorderWidthProperty,   Value = 2 },
                    new Setter { Property = Button.CornerRadiusProperty,   Value = 20 },
                    new Setter { Property = HeightRequestProperty,   Value = 40 },
                    new Setter { Property = WidthRequestProperty,   Value = 40 },
                    new Setter { Property = Button.FontAttributesProperty,   Value = FontAttributes.Bold }
            }
            };

            Resources = new ResourceDictionary();
            Resources.Add("unSelectedStyle", unselectedStyle);
            Resources.Add("selectedStyle", selectedStyle);
        }
    }
}