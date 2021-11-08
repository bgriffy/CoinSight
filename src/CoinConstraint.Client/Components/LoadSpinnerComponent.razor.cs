using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace CoinConstraint.Client.Components;

public partial class LoadSpinnerComponent
{
    [Parameter]
    public string LoadMessage { get; set; } = "Loading...";

    [Parameter]
    public bool LoadSpinnerIsHidden { get; set; }

    [Parameter]
    public EventCallback<bool> LoadSpinnerIsHiddenChanged { get; set; }

    public async Task ShowLoadSpinner(string loadMessage = "")
    {
        this.LoadSpinnerIsHidden = false; 
        this.LoadMessage = loadMessage;
        await LoadSpinnerIsHiddenChanged.InvokeAsync(this.LoadSpinnerIsHidden);
        StateHasChanged();
    }

    public async Task HideLoadSpinner()
    {
        this.LoadSpinnerIsHidden = true;
        await LoadSpinnerIsHiddenChanged.InvokeAsync(this.LoadSpinnerIsHidden);
        StateHasChanged();
    }
}
