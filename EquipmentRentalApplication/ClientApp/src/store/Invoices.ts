import { Action, Reducer } from 'redux';
import { AppThunkAction } from '.';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface InvoicesState {
    isLoading: boolean;
    startDateIndex?: number;
    invoices: Invoice[];
}

export interface Invoice {
    invoiceId: number;
    title: string;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestInvoicesAction {
    type: 'REQUEST_INVOICES';
    startDateIndex: number;
}

interface ReceiveInvoicesAction {
    type: 'RECEIVE_INVOICES';
    startDateIndex: number;
    invoices: Invoice[];
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestInvoicesAction | ReceiveInvoicesAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestInvoices: (startDateIndex: number): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.Invoices && startDateIndex !== appState.Invoices.startDateIndex) {
            fetch(`api/invoice`)
                .then(response => response.json() as Promise<Invoice[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_INVOICES', startDateIndex: startDateIndex, invoices: data });
                });

            dispatch({ type: 'REQUEST_INVOICES', startDateIndex: startDateIndex });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: InvoicesState = { invoices: [], isLoading: false };

export const reducer: Reducer<InvoicesState> = (state: InvoicesState | undefined, incomingAction: Action): InvoicesState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_INVOICES':
            return {
                startDateIndex: action.startDateIndex,
                invoices: state.invoices,
                isLoading: true
            };
        case 'RECEIVE_INVOICES':
            // Only accept the incoming data if it matches the most recent request. This ensures we correctly
            // handle out-of-order responses.
            if (action.startDateIndex === state.startDateIndex) {
                return {
                    startDateIndex: action.startDateIndex,
                    invoices: action.invoices,
                    isLoading: false
                };
            }
            break;
    }

    return state;
};
