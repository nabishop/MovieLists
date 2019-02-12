import { MovieModel } from './moviemodel';
import { SearchdMovie } from './searchedmovie';

export interface ListItem {
    "name": string,
    "dateAdded": string,
    "movie_id": number,
    "user_id": number,
    "movielist": Array<MovieModel>
    "searchlist": Array<SearchdMovie>
}