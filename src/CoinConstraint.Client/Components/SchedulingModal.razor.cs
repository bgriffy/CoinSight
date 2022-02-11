using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;

namespace CoinConstraint.Client.Components;

public partial class SchedulingModal
{
    private bool _isLargeScreen;
    private bool _isSmallScreen;
    private bool _isMediumScreen;
    private Blazorise.Modal _schedulingModal;
    private BudgetScheduleDetailModal _scheduleDetailModal;
    private BudgetSchedule _selectedSchedule;

    [Parameter]
    public Budget Budget { get; set; }

    [Parameter]
    public EventCallback SchedulesSaveRequested { get; set; }

    [Parameter]
    public EventCallback<BudgetSchedule> ScheduleDeleted { get; set; }

    [Parameter]
    public EventCallback ScheduleModified { get; set; }

    [Parameter]
    public EventCallback<BudgetSchedule> ScheduleAdded { get; set; }

    private void HandleNewSchedule(BudgetSchedule newSchedule)
    {
        newSchedule.BudgetID = (int)this.Budget.ID;
        this.Budget.BudgetSchedules.Add(newSchedule);
        this.Budget.IsUpdated = true;
    }

    private async Task HandleModifiedSchedule()
    {
        await ScheduleModified.InvokeAsync();
        StateHasChanged();
    }

    private async Task HandleDeletedSchedule(BudgetSchedule schedule)
    {
        await ScheduleDeleted.InvokeAsync(schedule);
    }

    public async Task Show()
    {
        await _schedulingModal.Show();
    }

    private async Task AddNewSchedule()
    {
        await _scheduleDetailModal.Show();
    }

    private async Task EditSchedule(BudgetSchedule schedule)
    {
        await _scheduleDetailModal.Show(schedule);
    }

    private async Task Save()
    {
        await _schedulingModal.Hide();
        await SchedulesSaveRequested.InvokeAsync();
    }
}
