using Fluxor;


namespace GEO_DROID.Store.Spinner
{
    public static class SpinnerReducers
    {
        [ReducerMethod]
        public static SpinnerState ReduceShowSpinner(SpinnerState state, ShowSpinnerAction action)
        {
            return state with
            {
                IsVisible = true,
                Title = action.title,
                Message = action.message,
            };
        }

        [ReducerMethod]
        public static SpinnerState ReduceHideSpinner(SpinnerState state, HideSpinnerAction action)
        {
            return state with
            {
                IsVisible = false,
                Title = null,
                Message = null
            };
        }
    }
}
