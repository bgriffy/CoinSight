using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace CoinSight.Client.Shared;

public partial class LoadSpinner
{
    [Parameter]
    public string LoadMessage { get; set; }

    [Parameter]
    public bool LoadSpinnerIsVisible { get; set; } = false;

    [Parameter]
    public EventCallback<bool> LoadSpinnerIsVisibleChanged { get; set; }

    public async Task ShowLoadSpinner(string loadMessage = "")
    {
        LoadSpinnerIsVisible = true;
        LoadMessage = loadMessage;
        await LoadSpinnerIsVisibleChanged.InvokeAsync(LoadSpinnerIsVisible);
        StateHasChanged();
    }

    public async Task HideLoadSpinner()
    {
        LoadSpinnerIsVisible = false;
        await LoadSpinnerIsVisibleChanged.InvokeAsync(LoadSpinnerIsVisible);
        StateHasChanged();
    }
}
