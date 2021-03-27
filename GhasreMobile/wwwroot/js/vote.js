// Vote List
const votelistKey = "votelist";
// [{id, isUp},{5, true}]
let votelist = JSON.parse(localStorage.getItem(votelistKey)) || [];

if (isUserLoggedIn) {
    if (votelist.length > 0) {
        for (let vote of votelist) {

            let val = vote.id;
            //#region buts
            var voteDOM = document.getElementById('Vote' + val);
            if (voteDOM) {
                if (vote.isUp) {
                    const up = voteDOM.parentElement.querySelector('.up');
                    up.classList.add('link-active');
                }
                else if (!vote.isUp) {
                    const down = voteDOM.parentElement.querySelector('.down');
                    down.classList.add('link-active');
                }
            }
            //#endregion
        }
    }
}

function VoteUp(val) {

    if (!isUserLoggedIn) {
        UIkit.notification('ابتدا وارد سایت شوید');
        return;
    }

    //#region buts

    var vote = document.getElementById('Vote' + val);
    const up = vote.parentElement.querySelector('.up');
    const down = vote.parentElement.querySelector('.down');

    //#endregion

    //#region localStorage

    let cacheVote = votelist.filter(vote => vote.id === val)[0];

    up.classList.remove('link-active');
    down.classList.remove('link-active');

    if (cacheVote) {

        if (cacheVote.isUp) {
            // If already Up
            // Do nothing
            up.classList.add('link-active');
        }
        else if (!cacheVote.isUp) {
            // If formerly Down
            // Clear Vote
            votelist.splice(votelist.indexOf(cacheVote), 1);
            dbVoteUp(val);
        }

    }
    else {
        // If no prioer Votes
        // Vote Up
        votelist.push({ id: val, isUp: true });
        up.classList.add('link-active');
        dbVoteUp(val);
    }


    // update local storage
    localStorage.setItem(votelistKey, JSON.stringify(votelist));

    //#endregion
}

function VoteDown(val) {

    if (!isUserLoggedIn) {
        UIkit.notification('ابتدا وارد سایت شوید');
        return;
    }

    //#region buts

    var vote = document.getElementById('Vote' + val);
    const up = vote.parentElement.querySelector('.up');
    const down = vote.parentElement.querySelector('.down');

    //#endregion

    //#region localStorage

    let cacheVote = votelist.filter(vote => vote.id === val)[0];

    up.classList.remove('link-active');
    down.classList.remove('link-active');

    if (cacheVote) {

        if (cacheVote.isUp) {
            // If formerly Up
            // Clear Vote
            votelist.splice(votelist.indexOf(cacheVote), 1);
            dbVoteDown(val);
        }
        else if (!cacheVote.isUp) {
            // If already Down
            // Do nothing
            down.classList.add('link-active');
        }

    }
    else {
        // If no prioer Votes
        // Vote Down
        votelist.push({ id: val, isUp: false });
        down.classList.add('link-active');
        dbVoteDown(val);
    }


    // update local storage
    localStorage.setItem(votelistKey, JSON.stringify(votelist));

    //#endregion


}

function dbVoteDown(val) {
    var vote = document.getElementById('Vote' + val);

    //var result = Bjax("/Forum/VoteDown?id=", val, "SP");

    //if (result) {
    //    vote.innerText = parseInt(vote.innerText) - 1;
    //} else {
    //    UIkit.notification('ابتدا وارد سایت شوید');
    //}

    vote.innerText = parseInt(vote.innerText) - 1;

    debugger;
    fetch("/Forum/VoteDown/" + val).then(i => {
        console.log(i);
    });


}

function dbVoteUp(val) {
    var vote = document.getElementById('Vote' + val);

    //var result = Bjax("/Forum/VoteUp?id=", val, "SP");

    //if (result) {
    //    vote.innerText = parseInt(vote.innerText) + 1;
    //} else {
    //    UIkit.notification('ابتدا وارد سایت شوید');
    //}

    var vote = document.getElementById('Vote' + val);
    vote.innerText = parseInt(vote.innerText) + 1;

    debugger;
    fetch("/Forum/VoteUp/" + val)
        .then(i => {
            console.log(i);
        });


}
