import { Link, useParams } from 'react-router-dom';
import products from '../data';
const SingleProduct = () => {
  const { productID } = useParams();
  const product = products.find((product) => product.id === productID);
  const { image, name } = product;
  return (
    <section className='section product'>
      <img src={image} alt={name}></img>
      <h2>{name}</h2>
      <Link to='/products'>back to products</Link>
    </section>
  );
};

export default SingleProduct;
