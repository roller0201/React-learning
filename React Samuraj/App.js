const header = <h1 className="red">Witaj na stronie</h1>;
const classBig = "big";
const handleClick = () => alert("klik");
const main = (
  <div>
    <h1 onClick={handleClick} className="red">
      Pierwszy artykuł
    </h1>
    <p>
      loremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremlorem
    </p>
  </div>
);
const text = "stopkaaaa";
const largeTxt = "loremsfdfdsfdfsdsdfsfsdffdssfdfsd";
const footer = (
  <footer>
    {largeTxt}
    <p className={classBig}>{text}</p>
  </footer>
);

const app = [header, main, footer];
ReactDom.render(app, document.getElementById("root"));
