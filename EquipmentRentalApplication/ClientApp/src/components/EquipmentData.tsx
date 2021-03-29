import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';
import { ApplicationState } from '../store/index';
import * as EquipmentRentalStore from '../store/EquipmentRental';

// At runtime, Redux will merge together...
type EquipmentRentalProps =
  EquipmentRentalStore.EquipmentRentalState // ... state we've requested from the Redux store
  & typeof EquipmentRentalStore.actionCreators // ... plus action creators we've requested
  & RouteComponentProps<{ startDateIndex: string }>; // ... plus incoming routing parameters


class EquipmentData extends React.PureComponent<EquipmentRentalProps> {
  // This method is called when the component is first added to the document
  public componentDidMount() {
    this.ensureDataFetched();
  }

  // This method is called when the route parameters change
  public componentDidUpdate() {
    this.ensureDataFetched();
  }

  public render() {
    return (
      <React.Fragment>
        <h1 id="tabelLabel">Equipment list</h1>
            <p>For individual machines, enter the number of days for how long he wishes to rent it, and click "Add to Cart".</p>
        {this.renderEquipmentsTable()}
            {this.renderPagination()}
            {this.renderCart()}

      </React.Fragment>
    );
  }

  private ensureDataFetched() {
    const startDateIndex = parseInt(this.props.match.params.startDateIndex, 10) || 0;
      this.props.requestEquipments(startDateIndex);
  }

  private renderEquipmentsTable() {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Equipment name</th>
            <th>Days to rent</th>
          </tr>
        </thead>
        <tbody>
          {this.props.equipments.map((equipments: EquipmentRentalStore.Equipment) =>
            <tr key={equipments.equipmentId}>
                  <td>{equipments.equipmentName}</td>
                  <td><input value={equipments.daysRent}/></td>
            </tr>
          )}
        </tbody>
        </table>
    );
  }

  private renderPagination() {
    const prevStartDateIndex = (this.props.startDateIndex || 0) - 5;
    const nextStartDateIndex = (this.props.startDateIndex || 0) + 5;

    return (
      <div className="d-flex justify-content-between">
        <Link className='btn btn-outline-secondary btn-sm' to={`/equipment-data/${prevStartDateIndex}`}>Previous</Link>
        {this.props.isLoading && <span>Loading...</span>}
        <Link className='btn btn-outline-secondary btn-sm' to={`/equipment-data/${nextStartDateIndex}`}>Next</Link>
      </div>
       );
    }

    private renderCart() {
        return (

            <div className="cart float-right" >
                <button type="button"
                    className="btn btn-primary btn-lg"
                    onClick={() => { this.props.addToCart(); }}>
                        Add to Cart
                </button>
            </div>
        );

    }
}

export default connect(
  (state: ApplicationState) => state.equipmentRental, // Selects which state properties are merged into the component's props
  EquipmentRentalStore.actionCreators // Selects which action creators are merged into the component's props
)(EquipmentData as any);
