import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import InvoiceData from './components/InvoiceData';
import EquipmentData from './components/EquipmentData';

import './custom.css'

export default () => (
    <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/invoice-data/:startDateIndex?' component={InvoiceData} />
        <Route path='/equipment-data/:startDateIndex?' component={EquipmentData} />
    </Layout>
);
