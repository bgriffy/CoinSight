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

    private async Task HandleNewSchedule(BudgetSchedule newSchedule)
    {
        await ScheduleAdded.InvokeAsync(newSchedule);
    }

    private async Task HandleModifiedSchedule()
    {
        await ScheduleModified.InvokeAsync();
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
        await SchedulesSaveRequested.InvokeAsync();
    }
}
