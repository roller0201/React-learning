//Funkcja strzałkowa
const fn = (item) => console.log("Podany argument to" + item);
fn("hej"); // Podany argument to hej

const fn1 = (item) => {
  return console.log("Podany argument to " + item);
};
//Metoda join zwraca stringa z tablicy, nie niszcząć tablicy
const users = ["adam", "bogdan", "czarek", "darek"];
const usersString = users.join(" ");
console.log(usersString); //adam bogdan czarek darek

//Metoda concat - łączymy tablice z innym elementem i zwracamy nową tablice
const users1 = ["adam", "bogdan", "czarek", "darek"];
const newUser = "edyta";
const allUsers = users1.concat(newUser);
console.log(allUsers); //['adam', 'bogdan', 'czarek', 'darek', 'edyta']

//Metoda spread - łączenie tablic
const users2 = ["adam", "bogdan", "czarek", "darek"];
const allUsers1 = [...users2, newUser];
console.log(allUsers1); //['adam', 'bogdan', 'czarek', 'darek', 'edyta']

///////////////////////////////////////////////////
//Metody irytujące po tablicach
//Metoda map zwraca nową tablice o tej samej długości, wykonuje pętle po całej tablicy
const users3 = ["adam", "bogdan", "czarek", "darek"];
const usersFirstLetterUpperCase = users3.map((user) => user[0].toUpperCase());
console.log(usersFirstLetterUpperCase); //['A', 'B', 'C', 'D']
const numbers = [2, 3, 4];
const doubleNumber = numbers.map((number) => number * 2);
console.log(doubleNumber); //[4, 6, 8]

//Metoda forEach - pracuje na tablicy, nie zwraca nowej (zwraca undefined)
const usersAge = [20, 21, 22, 23, 43];
usersAge.forEach((age) =>
  console.log(`W przyszłym roku użytkownik będzie miał ${age + 10} lat`)
);
let usersTotalAge = 0;
usersAge.forEach((age) => (usersTotalAge += age));
console.log(usersTotalAge); //129
const section = document.createElement("section");
usersAge.forEach((age, index, array) => {
  section.innerHTML += `<h1> Użytkownik ${index + 1}</h1>
    <p>wiek: ${age}</p>`;
  if (index === array.length - 1) {
    document.body.appendChild(section);
  }
});

//Metoda filter - zwraca nową tablice złożoną z tych elementów, przy których iterator zwrócł true
const nameWith6Letter = users.filter((user) => user.length === 6);
console.log(nameWith6Letter); //['bogdan', 'czarek']
const nameWithLetterK = users.filter((user) => {
  return user.indexOf("k") > -1;
});
console.log(nameWithLetterK); //['czarek', 'darek']

//Metoda findIndex zwraca indeks elementu, który jako pierwszy zwróci true(spełnia warunek).Jeśli w żadnej iteracji
//nie będzie spełnioniony warunek to zwróci -1
const customers = [
  { name: "Adam", age: 67 },
  { name: "Basia", age: 27 },
  { name: "Marta", age: 17 },
];
const isUsersAdult = customers.findIndex((customer) => customer.age < 18);
console.log(isUsersAdult); //2

//Metoda find zwraca element, który jako pierwszy zwróci true. Jeśli w żadnej iteracji nie spełni warunki to zwróci undefined.
const customers2 = [
  { name: "Adam", age: 67 },
  { name: "Basia", age: 27 },
  { name: "Marta", age: 17 },
];
const isUsersAdult2 = customers.find((customer) => customer.age >= 18);
console.log(isUsersAdult2); //{name: 'Adam', age: 67}

class Country {
  constructor(name) {
    this.name = name;
    this.showName = () => console.log(this.name);
  }
  showCountryName() {
    console.log(`Nazwa kraju to ${this.name}`);
  }
}
const Poland = new Country("Polska");
Poland.showCountryName(); //Nazwa kraju to Polska
Poland.showName(); // Polska

class Person {
  constructor(name) {
    this.name = name;
  }
  showName() {
    console.log(`Imię osoby to ${this.name}`);
  }
}
class Student extends Person {
  constructor(name = "", degrees = []) {
    super(name);
    this.degrees = degrees;
  }
  showDegress() {
    const completed = this.degrees.filter((degree) => degree > 2);
    console.log(
      `Student ${this.name} ma stopnie: ${this.degrees} i zaliczył już ${completed.length} przedmioty`
    );
  }
}
const Janek = new Student("Adam", [2, 3, 4, 5, 2, 3]);
Janek.showDegress(); //Student Adam ma stopnie: 2,3,4,5,2,3 i zaliczył już 4 przedmioty
//This keyword
//("use strict");
const fnc = function () {
  console.log(this);
};
fnc(); //undefined, use strict nie pozwala na domyslne dopisanie do window

const fnc2 = function () {
  console.log(this);
};
fnc2(); // window

const car = {
  brand: "opel",
  age: 2018,
  showAge() {
    console.log(`Samochód z ${this.age}`);
  },
  showBrand: () => {
    console.log(`Samochód marki ${this.brand}`);
  },
};
car.showAge(); //Samochód z 2018
car.showBrand(); //Samochód marki undefined Arrow function nie tworzy własnego this, tylko dziedziczy

const dog = {
  name: "rocky",
  showName() {
    console.log(`Imie psa to ${this.name}`);
  },
};
dog.showName(); //Imię psa to rocky
const dogName = dog.showName.bind(dog);
dogName(); // Wracamy do zakresu globalnego, który zwraca na this undefined
const cat = {
  kids: ["Lucek", "Łapciuch"],
  showKidsNames() {
    console.log(`Kot ma potomstwo ${this.kids}`);
    const showKidsNumber = () => {
      console.log(this.kids.length);
    }; //2
    showKidsNumber(); // błąd, this jest undefined, wywoluje się na obiekcie globalnym
  },
};
cat.showKidsNames(); //Kot ma potomstwo Lucek,Łapciuch
