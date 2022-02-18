using Blazor.ModalDialog;
using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;
using Microsoft.AspNetCore.Components.Web;

namespace CoinConstraint.Client.Components.Expenses;

public partial class ExpenseDetailModal
{
    private Blazorise.Modal _expenseModal;
    private Expense _expense;
    private bool _isDirty = false;

    [Parameter]
    public EventCallback<Expense> ExpenseAdded { get; set; }

    [Parameter]
    public EventCallback ExpenseUpdated { get; set; }


    public string ExpenseTitle
    {
        get => _expense.Title;
        set
        {
            _expense.Title = value;
            _isDirty = true;
        }
    }

    public string ExpenseDescription
    {
        get => _expense.Description;
        set
        {
            _expense.Description = value;
            _isDirty = true;
        }
    }

    public decimal ExpenseAmount
    {
        get => _expense.Amount;
        set
        {
            _expense.Amount = value;
            _isDirty = true;
        }
    }

    public DateTime ExpenseDueDate
    {
        get => _expense.DueDate;
        set
        {
            _expense.DueDate = value;
            _isDirty = true;
        }
    }

    public bool ExpensePay
    {
        get => _expense.Pay;
        set
        {
            _expense.Pay = value;
            _isDirty = true;
        }
    }

    public bool ExpensePaid
    {
        get => _expense.Paid;
        set
        {
            _expense.Paid = value;
            _isDirty = true;
        }
    }

    public bool ExpenseAutoPay
    {
        get => _expense.Automatic;
        set
        {
            _expense.Automatic = value;
            _isDirty = true;
        }
    }

    public string ExpensePaymentURL
    {
        get => _expense.PaymentURL;
        set
        {
            _expense.PaymentURL = value;
            _isDirty = true;
        }
    }

    public string ExpenseNote
    {
        get => _expense.Note;
        set
        {
            _expense.Note = value;
            _isDirty = true;
        }
    }

    public async Task HandleKeydownEvent(KeyboardEventArgs e)
    {
        if (e.Code == "Enter" || e.Code == "NumpadEnter")
        {
            await Save();
        }
    }

    protected override void OnInitialized()
    {
        _expense = new Expense();
    }

    public void ShowNewExpense()
    {
        _isDirty = false;
        _expense = new Expense();
        _expense.IsNew = true;
        _expenseModal.Show();
    }

    public void ShowExpense(Expense expense)
    {
        _isDirty = false;
        _expense = expense;
        _expenseModal.Show();
    }

    private async Task OnModalClosing(ModalClosingEventArgs e)
    {
        if (_isDirty)
        {
            var tmpMessage = "You have un-saved changes on this budget. Do you wish to continue without saving?";
            MessageBoxDialogResult result = await ModalDialog.ShowMessageBoxAsync("Coin Constraint", tmpMessage, MessageBoxButtons.YesNo);
            if (result == MessageBoxDialogResult.No) e.Cancel = true;
        }
    }

    private async Task Save()
    {
        if (_expense.IsNew)
        {
            await ExpenseAdded.InvokeAsync(_expense);
        }
        else
        {
            _expense.IsUpdated = true;
            await ExpenseUpdated.InvokeAsync();
        }
        _isDirty = false;
        _expenseModal.Hide();
    }

}
