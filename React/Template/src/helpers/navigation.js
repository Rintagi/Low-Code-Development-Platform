const blocker = []

export function registerBlocker(blockFn) {
    if (blocker.indexOf(blockFn) < 0) blocker.push(blockFn);
}

export function unregisterBlocker(blockFn) {
    const idx = blocker.indexOf(blockFn);
    if (blocker.indexOf(blockFn) >= 0) blocker.splice(idx,1);
}

export function getUserConfirmation(message, callback) {
    if (blocker.length > 0) return blocker[0](message,callback);
    else callback(window.confirm(message));
}

export function hasBlocker() { return blocker.length > 0;}