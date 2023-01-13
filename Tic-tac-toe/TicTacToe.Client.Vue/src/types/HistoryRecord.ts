import {GameStatus} from "@/types/GameStatus";

export interface HistoryRecord {
    id: string,
    playerX: string,
    playerO: string,
    gameStatus: GameStatus,
    gameResult: number
}
