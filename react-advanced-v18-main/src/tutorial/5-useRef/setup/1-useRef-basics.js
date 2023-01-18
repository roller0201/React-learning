import React, { useEffect, useRef } from 'react';

// preserves value
// DOES NOT trigger re-render
// target DOM nodes/elements

const UseRefBasics = () => {
  const refContainer = useRef(null);
  const divContainer = useRef(null);

  const handleSubmit = (e) => {
    e.preventDefault();
  };
  useEffect(() => {
    refContainer.current.focus();
  });
  return (
    <>
      <form className='form'>
        <div>
          <input type='text' ref={refContainer} />
          <button type='submit'>submit</button>
        </div>
      </form>
      <div ref={divContainer}>hello world</div>
    </>
  );
};

export default UseRefBasics;
