--FUNCTION_NAME--url = --URL--;
--FUNCTION_NAME--entries: --DATATYPE--[];

--FUNCTION_NAME--(--ARG_IDENTIFIERS--):Observable<--DATATYPE--[]>{
  const data = {
    --ARG_DEFINITIONS--
  };

  return this.http.post(this.url, JSON.stringify(data),
  {headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })}).pipe(
    map((res) => {
      this.entries = res['data']['entries'];
      return this.entries;
    }),
 );
}
