import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface EquipmentRentalState {
    isLoading: boolean;
    startDateIndex?: number;
    equipments: Equipment[];
}

export interface Equipment {
    equipmentId: number;
    equipmentName: string;
    equipmentTypeId: number;
    daysRent: number;
}

export interface Customer {
    customerId: number;
}

export interface Cart {
    client: Customer;
    cartItem: Equipment[];
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestEquipmentAction {
    type: 'REQUEST_EQUIPMENTS';
    startDateIndex: number;
}

interface ReceiveEquipmentsAction {
    type: 'RECEIVE_EQUIPMENTS';
    startDateIndex: number;
    equipments: Equipment[];
}




// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestEquipmentAction | ReceiveEquipmentsAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestEquipments: (startDateIndex: number): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.equipmentRental && startDateIndex !== appState.equipmentRental.startDateIndex) {
            fetch(`api/equipment`)
                .then(response => response.json() as Promise<Equipment[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_EQUIPMENTS', startDateIndex: startDateIndex, equipments: data });
                });

            dispatch({ type: 'REQUEST_EQUIPMENTS', startDateIndex: startDateIndex });
        }
    },
    addToCart: (cart: Cart): AppThunkAction<KnownAction> => (dispatch, getState) => { }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: EquipmentRentalState = { equipments: [], isLoading: false };

export const reducer: Reducer<EquipmentRentalState> = (state: EquipmentRentalState | undefined, incomingAction: Action): EquipmentRentalState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_EQUIPMENTS':
            return {
                startDateIndex: action.startDateIndex,
                equipments: state.equipments,
                isLoading: true
            };
        case 'RECEIVE_EQUIPMENTS':
            // Only accept the incoming data if it matches the most recent request. This ensures we correctly
            // handle out-of-order responses.
            if (action.startDateIndex === state.startDateIndex) {
                return {
                    startDateIndex: action.startDateIndex,
                    equipments: action.equipments,
                    isLoading: false
                };
            }
            break;
    }

    return state;
};
