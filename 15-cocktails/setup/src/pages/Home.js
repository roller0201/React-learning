import React from 'react';
import CocktailList from '../components/CocktailList';
import SearchForm from './SearchForm';

const Home = () => {
  return (
    <main>
      <SearchForm />
      <CocktailList />
    </main>
  );
};

export default Home;
