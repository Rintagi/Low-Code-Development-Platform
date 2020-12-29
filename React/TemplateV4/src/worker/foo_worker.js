import * as Comlink from 'comlink';

const obj = {
  counter: 1,
  inc() {
    console.log(this.counter);
    this.counter *= 2;
    return this.counter;
  }
};

Comlink.expose(obj);