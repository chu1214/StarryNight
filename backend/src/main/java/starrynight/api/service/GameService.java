package starrynight.api.service;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import starrynight.api.dto.game.StarcoinCountResponse;
import starrynight.api.dto.game.StarcoinListData;
import starrynight.api.dto.game.StarcoinListResponse;
import starrynight.api.dto.game.StoryListData;
import starrynight.db.entity.*;
import starrynight.db.repository.*;
import starrynight.enums.Check;
import starrynight.exception.CustomException;
import starrynight.exception.CustomExceptionList;

import javax.transaction.Transactional;
import java.sql.SQLOutput;
import java.util.ArrayList;
import java.util.List;

@Service
@Transactional
public class GameService {
    final MemberRepository memberRepository;
    final StarcoinRepository starcoinRepository;
    final MemberStarcoinRepository memberStarcoinRepository;
    final StoryRepository storyRepository;
    final MemberStoryRepository memberStoryRepository;

    public GameService(MemberRepository memberRepository, StarcoinRepository starcoinRepository, MemberStarcoinRepository memberStarcoinRepository,
                       StoryRepository storyRepository, MemberStoryRepository memberStoryRepository) {
        this.memberRepository = memberRepository;
        this.memberStarcoinRepository = memberStarcoinRepository;
        this.memberStoryRepository = memberStoryRepository;
        this.storyRepository = storyRepository;
        this.starcoinRepository = starcoinRepository;
    }

    public Long getStarcoinCount(Long id){
        Member member = findMemberById(id);
        return member.getStarcoinCount();
    }

    public StarcoinListResponse getStarcoinList(Long id, Long storyId){
        StarcoinListResponse starcoinListResponse = new StarcoinListResponse();
        Member member = findMemberById(id);
        List<Starcoin> starcoins = starcoinRepository.findAllByStoryId(storyId);
            starcoinListResponse.count = starcoins.size();
        starcoinListResponse.starcoins = new ArrayList();
        for(Starcoin starcoin : starcoins){
            //?????? ????????? ????????? ?????? ???????????? ????????? ??????
            MemberStarcoin memberStarcoin = memberStarcoinRepository.findByMemberIdAndStarcoinId(member.getId(), starcoin.getId());
            //?????? ????????? ??????
            starcoinListResponse.starcoins.add(new StarcoinListData(starcoin.getNum(), memberStarcoin.isTaken()));
        }

        return starcoinListResponse;
    }

    public boolean getStoryClear(Long id, String constellationEng){
        Member member = findMemberById(id);
        Story story = storyRepository.findByConstellationEng(constellationEng)
                .orElseThrow(() -> new CustomException(CustomExceptionList.STORY_NOT_FOUND));
        System.out.println("find story in get isStoryClear: " + story);
        MemberStory memberStory = memberStoryRepository.findByMemberIdAndStoryId(member.getId(), story.getId());
        return memberStory.isClear();
    }

    public List<StoryListData> getStoryList(Long id){
        Member member = findMemberById(id);
        List<StoryListData> storyListDatas = new ArrayList();
        //????????? ????????? ????????? DB?????? ????????????
        List<MemberStory> memberStories = memberStoryRepository.findAllByMemberId(member.getId());
        for(MemberStory memberStory : memberStories){
            //????????? ????????? DB??? ???????????? ??????

            //?????? ????????? ??????
            Story story = findStoryByMemberStory(memberStory);

            //?????? ???????????? ???????????? ?????? ??????
            List<Starcoin> starcoins = starcoinRepository.findAllByStoryId(story.getId());
            Long starcoinTotal = (long) starcoins.size();
            Long starcoinCurrent = (long) 0;
            for(Starcoin starcoin : starcoins){
                if(memberStarcoinRepository.findByMemberIdAndStarcoinId(member.getId(), starcoin.getId()).isTaken()){
                    starcoinCurrent++;
                }
            }
            //????????? ??????
            StoryListData storyListData = new StoryListData(
                    story.getTitle(),
                    story.getConstellation(),
                    story.getSummary(),
                    memberStory.isClear(),
                    starcoinCurrent,
                    starcoinTotal);

            //List ????????? ??????
            storyListDatas.add(storyListData);
        }
        System.out.println("HELLO4");
        //Response
        return storyListDatas;
    }
    public void setInitialGameInfor(Member member){
        List<Story> stories = storyRepository.findAll();    //????????? ???????????? ?????? ??????????????? ????????????
        List<MemberStory> memberStories = new ArrayList();
        for(Story story : stories){
            //????????? ????????? ??????????????? ???????????? ??????
            MemberStory memberStory = new MemberStory(member, story);
            memberStories.add(memberStory);

            //?????? ???????????? ??????????????? ??????
            setMemberCoinList(member, story.getId());
        }
        //DB??? ??????
        memberStoryRepository.saveAll(memberStories);
    }

    public void setMemberCoinList(Member member, Long storyId){
        //???????????? ??????????????? ???????????? ????????????
        List<Starcoin> starcoins = starcoinRepository.findAllByStoryId(storyId);
        List<MemberStarcoin> memberStarcoins = new ArrayList();
        for(Starcoin starcoin : starcoins){
            //????????? ?????????????????? ???????????? ?????????.
            memberStarcoins.add(new MemberStarcoin(member, starcoin));
        }
        //????????? ???????????? DB??? ??????
        memberStarcoinRepository.saveAll(memberStarcoins);
    }
    public Member findMemberById(Long id){
        return memberRepository.findById(id).orElseThrow(() ->
                new CustomException(CustomExceptionList.MEMBER_NOT_FOUND));
    }

    public Story findStoryByMemberStory(MemberStory memberStory){
        return storyRepository.findById(memberStory.getStory().getId()).orElseThrow(() ->
                new CustomException(CustomExceptionList.STORY_NOT_FOUND));

    }

    public void increaseStarcoinCount(Long id){
        Member member = findMemberById(id);
        member.setStarcoinCount(member.getStarcoinCount()+1);
        memberRepository.save(member);
        return;
    }

    public void updateStarcoinStatus(Long memberId, Long storyId, Long starcoinNum){
        //???????????? ????????????????????? ???????????? ???????????? ??????
        Starcoin starcoin = starcoinRepository.findByStoryIdAndNum(storyId, starcoinNum);
        //?????? ??????????????? memberStarcoin?????? ??????
        MemberStarcoin memberStarcoin = memberStarcoinRepository.findByMemberIdAndStarcoinId(memberId, starcoin.getId());
        memberStarcoin.setTaken(true);
        memberStarcoinRepository.save(memberStarcoin);
        return;
    }

    public void setStoryClear(Long id, Long storyId){
        MemberStory memberStory = memberStoryRepository.findByMemberIdAndStoryId(id, storyId);
        memberStory.setClear(true);
        memberStoryRepository.save(memberStory);
        return;
    }
}
