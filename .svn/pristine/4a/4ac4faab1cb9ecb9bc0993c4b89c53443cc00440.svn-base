using Fluxor;


namespace GEO_DROID.Store.Navegation
{

    public static class NavigationReducers
    {


        [ReducerMethod]
        public static NavigationState ChangeAllowNavegationAction(NavigationState State, ChangeAllowNavegationAction action)
        {

            return State with { AllowNavigation = action.allowNavegate };

        }

        [ReducerMethod]
        public static NavigationState ChangeIsNormalActionAction(NavigationState State, ChangeIsNormalAction action)
        {

            return State with { NormalNavegation = action.Normal };

        }

        [ReducerMethod]
        public static NavigationState ChangeNavigationHistoryAction(NavigationState State, ChangeNavigationHistory action)
        {

            Stack<string> stack = State.History;
            if (action.action == -1)
            {
                // Busco en el historial donde estoy actualmente 

            }
            else if (action.action == 1)
            {
                //añadimos la ruta al historial  
                stack.Push(action.Rute);
            }
            else
            {
                //no añadimos la ruta al historial 


            }
            return State with { History = stack };

        }


        [ReducerMethod]
        public static NavigationState SetCurrentRuteActionAction(NavigationState State, SetCurrentRuteAction action)
        {


            return State with { CurrentRoute = action.rute };

        }


    }


}
