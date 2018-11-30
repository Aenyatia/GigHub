var GigDetailsController = function (followingService) {
	var followButton;

	var fail = function () {
		alert("Something failed!");
	};

	var done = function () {
		const text = (followButton.text() === "Follow") ? "Following" : "Follow";

		followButton.text(text);
	};

	var toggleFollow = function (e) {
		followButton = $(e.target);
		const artistId = followButton.attr("data-user-id");

		if (followButton.text() === "Follow")
			followingService.createFollow(artistId, done, fail);
		else
			followingService.deleteFollow(artistId, done, fail);
	};

	const init = function () {
		$(".js-toggle-follow").click(toggleFollow);
	};

	return {
		init: init
	};

}(FollowingService);
