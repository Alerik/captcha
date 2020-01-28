export enum DatasetTypes{
    text_index,
    text_multi,
    image_multi
}
export class Dataset{
    constructor(
        public id: string,
        public name: string,
        public prompt: string,
        public settype: DatasetTypes,
        public requestedAccuracy: number,
        public entry_count:number,
        public annotations_total:number,
        public completion: number,
        public reviewed: boolean,
        public approved : boolean,
        public created : boolean,
        public description : string
    ){}
}