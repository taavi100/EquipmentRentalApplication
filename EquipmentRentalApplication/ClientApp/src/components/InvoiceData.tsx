import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';
import { ApplicationState } from '../store/index';
import * as InvoicesStore from '../store/Invoices';

// At runtime, Redux will merge together...
type WeatherForecastProps =
  InvoicesStore.InvoicesState // ... state we've requested from the Redux store
  & typeof InvoicesStore.actionCreators // ... plus action creators we've requested
  & RouteComponentProps<{ startDateIndex: string }>; // ... plus incoming routing parameters


class FetchData extends React.PureComponent<WeatherForecastProps> {
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
        <h1 id="tabelLabel">Customer Invoices</h1>
            <p>Customers can ask for an invoice that must be generated as a text file.</p>
        {this.renderInvocesList()}
        {this.renderPagination()}
      </React.Fragment>
    );
  }

  private ensureDataFetched() {
    const startDateIndex = parseInt(this.props.match.params.startDateIndex, 10) || 0;
    this.props.requestInvoices(startDateIndex);
  }

  private renderInvocesList() {
    return (
        <ul className="list-group invoice">
            {this.props.invoices.map((invoice: InvoicesStore.Invoice) =>
                <li className='list-group-item'><a href={'/api/invoice/' + invoice.invoiceId} className="btn btn-info" role="button">{invoice.title}</a></li>
             )}
        </ul>
    );
  }

  private renderPagination() {
    const prevStartDateIndex = (this.props.startDateIndex || 0) - 5;
    const nextStartDateIndex = (this.props.startDateIndex || 0) + 5;

    return (
      <div className="d-flex justify-content-between">
        <Link className='btn btn-outline-secondary btn-sm' to={`/invoice-data/${prevStartDateIndex}`}>Previous</Link>
        {this.props.isLoading && <span>Loading...</span>}
        <Link className='btn btn-outline-secondary btn-sm' to={`/invoice-data/${nextStartDateIndex}`}>Next</Link>
      </div>
    );
  }
}

export default connect(
  (state: ApplicationState) => state.Invoices, // Selects which state properties are merged into the component's props
  InvoicesStore.actionCreators // Selects which action creators are merged into the component's props
)(FetchData as any);
