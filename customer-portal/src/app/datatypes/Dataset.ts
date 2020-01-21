export enum DatasetTypes{
    text_index,
    text_multi,
    image_multi
}
export class Dataset{
    constructor(
        id: string,
        name: string,
        prompt: string,
        settype: DatasetTypes,
        requestedAccuracy: number,
        entry_count:number,
        annotations_total:number,
        completion: number,
        reviewed: boolean,
        approved : boolean,
        created : boolean,
        description : string
    ){}
}