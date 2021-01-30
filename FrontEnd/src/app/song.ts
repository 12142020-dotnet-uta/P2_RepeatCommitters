export class Song
{
    public id:number;
    public name: string;
    public artist: string;
    public album: string;
    public albumURL: string;
    public year: number;
    public genre: string;
    public youtubeURL: string;
    public lyrics: string;
    public similarSongs: Array<Song>;

    constructor(i:number, n: string, art: string, al: string, aurl: string, g: string, y: number, yurl: string)
    {
        this.id = i;
        this.name = n;
        this.artist = art;
        this.album = al;
        this.albumURL = aurl;
        this.genre = g;
        this.year = y;
        this.youtubeURL = yurl;
        this.similarSongs = new Array<Song>();
        this.lyrics = "";
    }
}