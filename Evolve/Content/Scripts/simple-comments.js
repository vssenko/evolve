function CommentBox(args)
{
    var downloadUrl = args.downloadUrl;
    var uploadUrl = args.uploadUrl;
    var commentContainer = '<ul class="media-list"><li class="media">{{Comments}}</li></ul>';
    var commentTemplate = '<div class="media-left"> \
        <a href="/user/{{UserId}}"><img class="media-object" src="{{UserPicture}}"></a></div> \
        <div class="media-body"><h4 class="media-heading">{{Header}}</h4><p>{{Body}}</p>{{Answers}}</div>';
    var result = '';
    var create = function()
    {
        $.ajax({
            method: "POST",
            url: downloadUrl,
            success: function(data)//retrieve comments json
            {

            }
        });
    }
}