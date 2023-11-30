export type DotNetHelperType = {
  invokeMethodAsync<T>(methodIdentifier: string, ...args: unknown[]): Promise<T>
}
