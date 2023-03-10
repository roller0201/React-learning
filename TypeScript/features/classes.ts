class Vehicle {
  constructor(public color: string) {}
  public drive(): void {
    console.log('chugga chugga');
  }
  protected honk(): void {
    console.log('beep');
  }
}

const vehicle = new Vehicle('orange');
console.log(vehicle.color);

class Car extends Vehicle {
  constructor(public wheels: number, color: string) {
    super('red');
  }
  drive(): void {
    console.log('vroom');
  }
  startDrivingProcess(): void {
    this.drive();
    this.honk();
  }
}

const car = new Car(12, 'red');
car.startDrivingProcess();
