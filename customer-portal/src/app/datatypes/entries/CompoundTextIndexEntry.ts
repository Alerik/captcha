import { Text_Index_Entry } from "./TextIndexEntry";
import { Text_Index_Annotation } from "../annotations/TextIndexAnnotation";

export class CompoundIndexEntry{
    constructor(
    public entry : Text_Index_Entry,
    public annotations : Text_Index_Annotation[]){}
}