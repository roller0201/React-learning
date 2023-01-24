import axios from 'axios';

const authfFtch = axios.create({
  baseUrl: 'https://course-api.com',
  headers: {
    Accept: 'application/json',
  },
});

export default authFetch;
