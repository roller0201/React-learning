import { combineReducers } from 'redux';
import reducer from './repositoriesReducers';

const reducers = combineReducers({
  repositories: reducer,
});

export default reducers;

export type RootState = ReturnType<typeof reducers>;
