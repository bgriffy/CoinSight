namespace CoinConstraint.Client.Components;

public partial class LoadSpinner
{
    [Parameter]
    public string LoadMessage { get; set; }

    [Parameter]
    public bool LoadSpinnerIsHidden { get; set; } = true;

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
