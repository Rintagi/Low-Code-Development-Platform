import * as Comlink from 'comlink';

const obj = {
  counter: 0,
  inc() {
    console.log(this.counter);
    this.counter++;
    return this.counter;
  }
};

Comlink.expose(obj);