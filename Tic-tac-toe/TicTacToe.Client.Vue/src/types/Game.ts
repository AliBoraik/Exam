import {GameResult} from "@/types/GameResult";
import {GameStatus} from "@/types/GameStatus";

export type Game = {
    PlayerX?: string,
    PlayerO?: string,
    GameStatus: GameStatus,
    GameResult?: GameResult;
}