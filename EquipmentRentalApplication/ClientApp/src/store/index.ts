import * as Invoices from './Invoices';
import * as EquipmentRental from './EquipmentRental';
import * as Counter from './Counter';

// The top-level state object
export interface ApplicationState {
    counter: Counter.CounterState | undefined;
    Invoices: Invoices.InvoicesState | undefined;
    equipmentRental: EquipmentRental.EquipmentRentalState | undefined;
}

// Whenever an action is dispatched, Redux will update each top-level application state property using
// the reducer with the matching name. It's important that the names match exactly, and that the reducer
// acts on the corresponding ApplicationState property type.
export const reducers = {
    counter: Counter.reducer,
    Invoices: Invoices.reducer,
    equipmentRental: EquipmentRental.reducer
};

// This type can be used as a hint on action creators so that its 'dispatch' and 'getState' params are
// correctly typed to match your store.
export interface AppThunkAction<TAction> {
    (dispatch: (action: TAction) => void, getState: () => ApplicationState): void;
}
