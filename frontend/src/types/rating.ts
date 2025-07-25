export type Rating = {

bookId : string
score : ScoreEnum
comment : string

};

export type CommentRating = {

fullName: string
email: string
ratingValue : ScoreEnum
comment : string

};


export enum ScoreEnum
    {
   
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5
    }
