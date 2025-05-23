using Fluxor;

namespace GEO_DROID.Store.Confirmation
{

    public record ConfirmationDialogData(string Title, string Message, TaskCompletionSource<bool> CompletionSource);

    //[FeatureState]
    public record ConfirmationState
    {
        public bool IsVisible { get; init; }
        public ConfirmationDialogRequest? CurrentDialog { get; init; }
    }
    public class ConfirmationDialogRequest
    {
        public string Title { get; }
        public string Message { get; }
        public TaskCompletionSource<bool> CompletionSource { get; }

        public ConfirmationDialogRequest(string title, string message)
        {
            Title = title;
            Message = message;
            CompletionSource = new TaskCompletionSource<bool>();
        }
    }
}
