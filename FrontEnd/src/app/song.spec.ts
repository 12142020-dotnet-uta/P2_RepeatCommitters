import { Song } from './song';

describe('User', () => {
  it('should create an instance', () => {
    expect(new Song("","","", "", 1, "", true)).toBeTruthy();
  });
});
