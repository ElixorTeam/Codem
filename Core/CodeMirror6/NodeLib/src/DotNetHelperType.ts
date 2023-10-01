export type DotNetHelperType = {
    invokeMethodAsync<T>(methodIdentifier: string, ...args: any[]): Promise<T>;
} 