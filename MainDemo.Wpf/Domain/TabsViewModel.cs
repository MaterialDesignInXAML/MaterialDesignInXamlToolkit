using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MaterialDesignDemo.Domain;

internal class TabsViewModel : ViewModelBase
{
    public ObservableCollection<CustomTab> CustomTabs { get; }

    public CustomTab? SelectedTab { get; set; }

    public string? VeryLongText { get; set; } = @"
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec dictum eleifend tellus, quis accumsan neque facilisis ac. Pellentesque non dignissim nisl, id egestas mauris. Suspendisse dui nisi, vestibulum et tincidunt sed, porta id lorem. Quisque iaculis nulla eget feugiat tristique. Suspendisse risus justo, condimentum sed venenatis id, aliquam at lectus. Fusce ut commodo dui. Vivamus eget iaculis sapien. Suspendisse tincidunt, lectus sed pretium porttitor, tortor odio vehicula sem, sed euismod ante erat vitae velit. Quisque maximus ut sem non imperdiet. Ut non libero sit amet risus fringilla convallis vitae sed turpis.

Duis aliquet nibh magna, et ultrices tortor rhoncus id. Morbi sed gravida neque. Nulla ornare posuere nisi, et pulvinar nibh euismod a. Nam aliquam ullamcorper congue. Sed sagittis hendrerit leo vitae lacinia. Integer lobortis, orci quis mattis venenatis, felis quam condimentum lorem, sit amet lacinia orci mi eu felis. Suspendisse potenti. Cras viverra tellus odio, in facilisis urna ornare vitae. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nullam sem nunc, posuere at tortor ut, iaculis dictum nisl. Cras blandit in ligula nec imperdiet. Maecenas molestie velit in sapien feugiat, in euismod lectus varius. Mauris quis accumsan arcu.

Donec massa est, pretium id nibh sed, varius luctus metus. Vivamus finibus placerat est sit amet molestie. In tincidunt enim a rhoncus viverra. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nam accumsan, erat vel consectetur tincidunt, mi turpis pulvinar mauris, at ultricies purus massa at mi. Etiam non tristique erat, eu ultricies nisi. In imperdiet sollicitudin pulvinar. Aliquam non nibh nunc. Vestibulum hendrerit libero eu felis aliquam, et ornare leo pharetra. Cras semper rutrum lectus at venenatis. Ut facilisis rutrum felis, eget facilisis neque gravida quis. Vestibulum ornare mollis pharetra.

Sed placerat sapien non quam fringilla fermentum. Nullam ex leo, condimentum sit amet magna vitae, condimentum volutpat risus. Aenean scelerisque neque cursus consequat elementum. Proin id tortor nec risus lacinia porta. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam massa ligula, condimentum pellentesque lacus id, ornare accumsan lectus. Fusce viverra nunc sit amet maximus tincidunt. Sed dapibus nulla eget tempus euismod. Aliquam fringilla turpis ut fringilla efficitur. Cras posuere suscipit ligula eget posuere. In aliquet non ligula at consequat. Ut venenatis nunc quis est congue, consequat rutrum justo vehicula. Cras tortor ante, condimentum non venenatis non, venenatis efficitur ex.

Nulla a porta libero, quis hendrerit ex. In ut pharetra sem. Nunc gravida ante rhoncus commodo aliquet. Integer luctus blandit libero, sed faucibus ligula ornare ut. Mauris facilisis, urna eu fermentum mollis, mauris massa commodo odio, a venenatis nunc nunc sollicitudin nibh. Ut mattis ipsum nec lacus mattis fringilla. Proin vulputate id velit a finibus. Ut nunc ex, elementum porttitor finibus vel, pellentesque vel turpis. Cras fringilla eleifend libero, ac feugiat arcu vehicula ornare. Nullam pretium finibus blandit. Etiam at urna facilisis, posuere felis non, congue velit. Pellentesque tortor erat, mattis at augue eu, egestas interdum nunc. Aliquam tortor lorem, venenatis eget vestibulum vitae, maximus eget nunc. Vestibulum et leo venenatis, rutrum lacus eget, mattis quam.";

    public TabsViewModel()
    {
        var closeCommand = new AnotherCommandImplementation(_ =>
        {
            if (SelectedTab is { } selectedTab)
                CustomTabs?.Remove(selectedTab);
        });

        CustomTabs = new()
        {
            new CustomTab(closeCommand)
            {
                CustomHeader = "Custom tab 1",
                CustomContent = "Custom content 1"
            },
            new CustomTab(closeCommand)
            {
                CustomHeader = "Custom tab 2",
                CustomContent = "Custom content 2"
            },
            new CustomTab(closeCommand)
            {
                CustomHeader = "Custom tab 3",
                CustomContent = "Custom content 3",
            },
        };
    }

}

internal partial class CustomTab : ObservableObject
{
    public ICommand CloseCommand { get; }

    public CustomTab(ICommand closeCommand) => CloseCommand = closeCommand;

    [ObservableProperty]
    private string? _customHeader;

    [ObservableProperty]
    private string? _customContent;

}
