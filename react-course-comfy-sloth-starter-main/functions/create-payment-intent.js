// domain/.netlify/functions/create-payment-intent
require('dotenv').config();

const stripe = require('stripe')(
  'sk_test_51MXSUFKdJcA5p46ZjCZ5Dg3Pfv6rIo6iDsWuImiz6GbQUwzBxN4snTanvtbJgQbDFDk3RXk4aiEepAIJ2Pv4hhoy00zb0lOjBU'
);

exports.handler = async function (event, context) {
  if (event.body) {
    const { cart, shipping_fee, total_amount } = JSON.parse(event.body);
    //console.log(cart);

    const calculateOrderAmount = () => {
      return shipping_fee + total_amount;
    };
    try {
      const paymentIntent = await stripe.paymentIntents.create({
        amount: calculateOrderAmount(),
        currency: 'usd',
      });
      return {
        statusCode: 200,
        body: JSON.stringify({ clientSecret: paymentIntent.client_secret }),
      };
    } catch (error) {
      return {
        statusCode: 500,
        body: JSON.stringify({ msg: error.message }),
      };
    }
  }
  return {
    statusCode: 200,
    body: 'Create Payment Intent',
  };
};
