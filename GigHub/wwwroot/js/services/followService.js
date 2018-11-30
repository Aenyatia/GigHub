var FollowingService = function () {
	const createFollow = function (artistId, done, fail) {
		$.ajax({
			url: "/api/followings",
			method: "POST",
			contentType: "application/json",
			data: JSON.stringify({
				followeeId: artistId
			})
		})
			.done(done)
			.fail(fail);
	};

	const deleteFollow = function (artistId, done, fail) {
		$.ajax({
			url: `/api/followings/${artistId}`,
			method: "POST"
		})
			.done(done)
			.fail(fail);
	};

	return {
		createFollow: createFollow,
		deleteFollow: deleteFollow
	};

}();
