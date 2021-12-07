# WPF adaptive design
 Adaptive design to WPF - my aproach to changing WPF App layout according to Window's size using DataTriggers and Styles.

# How does it look?

![wpf-adaptive-layout](https://user-images.githubusercontent.com/27814917/145099461-d3a792f5-48ea-4531-9726-a6a80add5d20.gif)

# How it works? Step by step 

## First things first 
Main idea is to write DataTemplates (whole Layouts) and according to Window's (or UserControl's) size (ActualWidth for example) change them in runtime. 

## Setup DataTemplate
First, we need to dive in code of UI element which we want to make adaptive. Next, in Resources we need to specify our DataTemplates:

```xaml
<UserControl.Resources>

    <DataTemplate x:Key="BigList">
        <!-- LAYOUT FOR BIG LIST  -->
        <ScrollViewer VerticalScrollBarVisibility="Auto">
            <StackPanel Background="LightGray">
                <local:ListItem Height="30" Margin="0,10" />
                <local:ListItem Height="30" Margin="0,10" />
                <local:ListItem Height="30" Margin="0,10" />
                <local:ListItem Height="30" Margin="0,10" />
                <local:ListItem Height="30" Margin="0,10" />
            </StackPanel>
        </ScrollViewer>
    </DataTemplate>

    <DataTemplate x:Key="SmallList">
        <!-- LAYOUT FOR SMALL LIST  -->
        <ScrollViewer VerticalScrollBarVisibility="Auto">
            <StackPanel Background="LightGray">
                <local:ListItem Height="20" Margin="0,2" />
                <local:ListItem Height="20" Margin="0,2" />
                <local:ListItem Height="20" Margin="0,2" />
                <local:ListItem Height="20" Margin="0,2" />
                <local:ListItem Height="20" Margin="0,2" />
            </StackPanel>
        </ScrollViewer>
    </DataTemplate>

</UserControl.Resources>
```

As you can see, DataTemplate is just our layout.

## Setup Converter
In your project we need custom ValueConverter, to check if our ActualWidth (or Height) is smaller, than breakpoint. You can write your own converter (for example, if you want to check if ActualWidth is GREATER than breakpoint, instead of checking if it's smaller), or use this:

```c#
public class BreakpointConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        double actual = (double)value;
        int breakpoint = int.Parse(parameter.ToString());

        return actual < breakpoint;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
```

And... don't forget to add your Converter as a Resource!
```xaml
<Application.Resources>
    <converters:BreakpointConverter x:Key="BreakpointConverter" />
</Application.Resources>
```

## Setup Style
So, we have our Layouts (DataTemplates) and converter... now it's time to use them!

We need style in your adaptive control, which TargetType will be "ContentControl". In Setter with property ContentTemplate we can set default layout. 

```xaml
<UserControl.Style>
    <Style TargetType="ContentControl">
        <Setter Property="ContentTemplate" Value="{StaticResource BigList}" />
        <Style.Triggers>
            <DataTrigger Binding="{Binding ActualWidth, RelativeSource={RelativeSource Self}, Converter={StaticResource BreakpointConverter}, ConverterParameter=250}" Value="True">
                <Setter Property="ContentTemplate" Value="{StaticResource SmallList}" />
            </DataTrigger>
        </Style.Triggers>

    </Style>
</UserControl.Style>
```

In Style Triggers we need to specify DataTriggers (something like media queries in CSS). As you can see, in our Trigger we have:
- Binding to ActualWidth - Property which we want to check
- RelativeSource Self - Property telling, that Setters in this Trigger will affect this control ("self")
- our Converter
- ConverterParameter - Property which is our breakpoint

If you use converter from this repo, this wole code is just checking if Binding Property is smaller than ConverterParameter. If it is, in Setter we set ContentTemplate of our Control to SmallList layout (DataTemplate).

This isn't of course whole code for layout from gif, but I hope it'll show you, how to implement something like this to your own app. If you want to see other DataTemplates (or generally, code of this demo), feel free to dive into this repo ;3

# Sources
[Stackoverflow question, which inspired me quite a lot ;pp](https://stackoverflow.com/questions/14013959/how-can-i-create-an-adaptive-layout-in-wpf#comment19354884_14013959)
