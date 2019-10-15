export class AsyncActionType {
    constructor(prefix, actionType) {
        const prefixedActionType = prefix ? prefix + "_" + actionType : actionType;
        this.ActionType = actionType;
        this.Prefix = prefix;
        this.PrefixedActionType = prefixedActionType;
        this.STARTED = prefixedActionType+'.STARTED';
        this.SUCCEEDED = prefixedActionType+'.SUCCEEDED';
        this.FAILED = prefixedActionType+'.FAILED';
        this.ENDED = prefixedActionType+'.ENDED';
    }
    bindActionReducer(reducers,async) {
        const combined = typeof reducers === "function";
        if (async) {
            return {
                [this.STARTED]:combined ? reducers : reducers.STARTED,
                [this.SUCCEEDED]:combined ? reducers : reducers.SUCCEEDED,
                [this.FAILED]:combined ? reducers : reducers.FAILED,
                [this.ENDED]:combined ? reducers : reducers.ENDED,
            }
        }
        else {
            return {
                [this.PrefixedActionType]: reducers,
            }
        }
    }
}
export const getAsyncTypes = (prefix,actionType) => new AsyncActionType(prefix,actionType); 