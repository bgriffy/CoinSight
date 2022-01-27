using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;

namespace CoinConstraint.Client.Components;

public partial class NoteDatagrid
{
    [Parameter]
    public Note SelectedNote { get; set; }

    [Parameter]
    public List<Note> Notes { get; set; }

    [Parameter]
    public EventCallback NewNoteModalRequested { get; set; }

    [Parameter]
    public EventCallback<Note> NoteDeletionRequested { get; set; }

    public async Task RequestNewNoteModal()
    {
        await NewNoteModalRequested.InvokeAsync();
    }

    public async Task RequestNoteDeletion()
    {
        await NoteDeletionRequested.InvokeAsync(this.SelectedNote);
    }
}
