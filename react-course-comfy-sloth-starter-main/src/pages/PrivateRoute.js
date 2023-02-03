import React from 'react';
import { Navigate } from 'react-router-dom';
import { useAuth0 } from '@auth0/auth0-react';
import Nav from '../components/Navbar';

const PrivateRoute = ({ children }) => {
  const { user } = useAuth0;
  if (!user) {
    return <Navigate to='/' />;
  }
  return children;
};
export default PrivateRoute;
