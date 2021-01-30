export class Song
{
    public id:number;
    public title: string;
    public artistName: string;
    public album: string;
    public year: number;
    public genre: string;
    public urlPath: string;
    public numberOfPlays: number;
    public isOriginal: boolean;
    
    //Optional
    public duration: string;
    public albumURL: string;
    public lyrics: string;
    //public similarSongs: Array<Song>;

    constructor(t: string, art: string, al: string, g: string, y: number, url: string, o: boolean)
    {
        this.title = t;
        this.artistName = art; 
        this.album = al;
        this.genre = g;
        this.year = y;
        this.urlPath = url;
        this.isOriginal = o;

        this.numberOfPlays = 0;
        this.lyrics = "";
        //Album URL???
    }
}