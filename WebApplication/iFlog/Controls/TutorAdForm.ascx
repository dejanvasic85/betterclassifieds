<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="TutorAdForm.ascx.vb" Inherits="BetterclassifiedsWeb.TutorAdForm" %>

<div class="well">
    <strong>New:</strong> These tutoring details that will be displayed 
    and searchable online!
</div>

<div class="formcontrol-container">
    <label>Course Subject(s)</label>
    <label class="helptext">What areas are covered in the course?</label>
    <paramountItCommon:HelpContextControl runat="server" Position="Bottom" ID="hlpTutorSubject" ImageUrl="~/Resources/Images/question_button.gif">
        <ContentTemplate>
            <span class="text-wrapper">Type a subject and press TAB or comma to save.</span>
        </ContentTemplate>
    </paramountItCommon:HelpContextControl>
    <div class="control">
        <paramountIt:TagsInput runat="server" ID="txtSubjects" />
    </div>
</div>

<script type="text/javascript">
    $(function () {
        $('.tags').tagsInput({
            width: '550px',
            height: '15px',
            onAddTag: function (tag) {
                var hiddenInput = $(this).closest('span').find('.hdnInputTag');
                var tags = hiddenInput.val();
                if (tags == '') {
                    tags = tag;
                } else {
                    tags = tags + "," + tag;
                }
                hiddenInput.val(tags);
            },
            onRemoveTag: function (tag) {
                var hiddenInput = $(this).closest('span').find('.hdnInputTag');
                var tags = hiddenInput.val().split(','),
                    index = tags.indexOf(tag);
                while (index >= 0) {
                    tags.splice(index, 1);
                    index = tags.indexOf(tag);
                }
                hiddenInput.val(tags.join(','));
            }
        });
    });
</script>
